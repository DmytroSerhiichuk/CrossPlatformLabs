const request = require('supertest');
const { exec } = require('child_process');

const apiurl = 'http://localhost:3001';

const dbType = process.argv.find(arg => arg.startsWith('--DbType='))?.split('=')[1] || 'InMemory';

let dotnetProcess;

beforeAll(async () => {
    console.log('Starting dotnet project...');
    
    dotnetProcess = exec(`dotnet run --project ./../Lab_6/Lab_6.csproj -- --DbType=${dbType} --SkipAuth=true`);
  
    await new Promise((resolve, reject) => {
      dotnetProcess.stdout.on('data', (data) => {
        console.log(data);
        if (data.includes('Content root path:')) {
          console.log('Dotnet project is up and running');
          resolve();
        }
      });
  
      dotnetProcess.on('error', (err) => {
        console.error('Error starting dotnet project:', err);
        reject(err);
      });
    });
  });
  
  afterAll(() => {
    console.log('Stopping dotnet project...');
    dotnetProcess.kill();
  });

jest.setTimeout(60000);

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
});
