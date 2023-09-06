/** @type {import('next').NextConfig} */

const nextConfig = {
  env: {
    MONGODB_URI: "mongodb+srv://saturniatechnology:stQYsuK2mszwi5Qf@clusterlp.mp08uox.mongodb.net",
    STRIPE_SK_DEV: "sk_test_51KYzCJK7hYD3CgEUdYgo3IIkhKLcnH3KdTf1ZuPqiWXaMsVrqiHPhuAOGsGoFSju6fZtJwKKm3mW5HAxrWcFxeo000LD8izMPF",
    STRIPE_SK_PRODUCTION: "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"
  },

  webpack: (config) => {
    config.module.rules.push({
      test: /\.svg$/,
      use: ["@svgr/webpack"],
    });

    return config;
  }
}

module.exports = nextConfig
