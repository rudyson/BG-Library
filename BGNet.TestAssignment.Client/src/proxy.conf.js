const PROXY_CONFIG = [
  {
    context: [
      "/api/*"
    ],
    target: "http://localhost:44080",
    secure: false,
    logLevel : "debug"
  }
]

module.exports = PROXY_CONFIG;
