const request = require('supertest');
const { spawn } = require('child_process');

const apiurl = 'http://localhost:3001';

const dbType = process.argv.find(arg => arg.startsWith('--DbType='))?.split('=')[1] || 'InMemory';

let dotnetProcess;

beforeAll(async () => {
  console.log('Starting dotnet project...');

  dotnetProcess = spawn('dotnet', [
    'run',
    '--project',
    './../Lab_6/Lab_6.csproj',
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
    dotnetProcess.kill('SIGINT');
    await new Promise((resolve) => {
      dotnetProcess.on('close', (code) => {
        console.log(`Dotnet project exited with code ${code}`);
        resolve();
      });
    });
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
