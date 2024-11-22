const request = require('supertest');
const { spawn } = require('child_process');
const treeKill = require('tree-kill'); 

const apiurl = 'http://localhost:3001';

const dbType = process.argv.find(arg => arg.startsWith('--DbType='))?.split('=')[1] || 'InMemory';
const path = process.argv.find(arg => arg.startsWith('--Lab6Path='))?.split('=')[1] || './../Lab_6/Lab_6.csproj';

let dotnetProcess;

beforeAll(async () => {
  console.log('Starting dotnet project...');

  dotnetProcess = spawn('dotnet', [
    'run',
    '--project',
    path,
    `--DbType=${dbType}`,
    '--SkipAuth=true',
  ]);

  dotnetProcess.stdout.on('data', (data) => {
    console.log(`[stdout]: ${data}`);
  });

  dotnetProcess.stderr.on('data', (data) => {
    console.error(`[stderr]: ${data}`);
  });

  dotnetProcess.on('error', (err) => {
    console.error('Error starting dotnet project:', err);
    throw err;
  });

  await new Promise((resolve, reject) => {
    const timeout = setTimeout(() => {
      reject(new Error('Timeout: Dotnet project did not start'));
    }, 120000);

    dotnetProcess.stdout.on('data', (data) => {
      if (data.includes('Content root path:')) {
        console.log('Dotnet project is up and running');
        clearTimeout(timeout);
        resolve();
      }
    });
  });
});

afterAll(async () => {
  console.log('Stopping dotnet project...');
  if (dotnetProcess) {
    try {
      await new Promise((resolve) => {
        treeKill(dotnetProcess.pid, 'SIGKILL', (err) => {
          if (err) {
            console.error('Error killing dotnet process:', err);
          } else {
            console.log('Dotnet process killed successfully.');
          }
          resolve();
        });
      });
    } catch (error) {
      console.error('Failed to kill process:', error);
    }

    dotnetProcess.stdout.destroy();
    dotnetProcess.stderr.destroy();
  }
});

jest.setTimeout(120000);

describe(`API Tests with ${dbType}`, () => {
  test('GET /api/customer - Should return 200', async () => {
    const response = await request(apiurl).get('/api/customer');
    expect(response.statusCode).toBe(200);
  });
  test('GET /api/v2.0/booking/3 - Api v2.0 should return booking.vehicle.manufacturer', async () => {
    const response = await request(apiurl).get('/api/v2.0/booking/3');
    expect(response.body.vehicle.manufacturerCode).toBeTruthy();
    expect(response.statusCode).toBe(200);
  });

  test('GET /api/v1.0/booking/3 - Api v1.0 should return booking.vehicle.manufacturer as null', async () => {
    const response = await request(apiurl).get('/api/v1.0/booking/3');
    expect(response.body.vehicle.manufacturerCode).toBeNull();
    expect(response.statusCode).toBe(200);
  });

  test('POST /api/booking-status - Should create new entity and return 201', async () => {
    const code = 'TTT';
    const description = 'TEST CODE';

    const response = await request(apiurl).post('/api/booking-status').send({
      'Code': code,
      'Description': description
    });
    expect(response.body.code).toBe(code);
    expect(response.body.description).toBe(description);
    expect(response.statusCode).toBe(201);
  });
});
