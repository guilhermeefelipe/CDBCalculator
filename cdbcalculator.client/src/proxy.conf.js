const { env } = require('process');

if (env.ASPNETCORE_HTTPS_PORT) {
  target = `https://localhost:${env.ASPNETCORE_HTTPS_PORT}`;
} else if (env.ASPNETCORE_URLS) {
  target = env.ASPNETCORE_URLS.split(';')[0];
} else {
  target = 'http://localhost:5222';
}

const PROXY_CONFIG = [
  {
    context: [
      "/weatherforecast",
      "/api/cdbcalculator" 
    ],
    target: target,
    secure: false,
    LogLevel: "debug",
    ws: true,
    headers: {
      Connection: 'Keep-Alive'
    }
  }
];

module.exports = PROXY_CONFIG;
