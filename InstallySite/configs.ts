const domain = process.env.NODE_ENV === "development" ? "http://localhost:3000" : "https://criarpagina.com";

const storage = {
  bucket: "formulario-clientes",
  baseUrl: "https://storage.googleapis.com"
}

export default {
  stripe: {
    publicKeyDevelopment: "pk_test_51KYzCJK7hYD3CgEU3iOB8Qa0dJ01KEWGj1PRf2jC40PauIxqUyUwUj8ZfgmTMCsjBEPZEJhtYnuQu2ue9PrVU0V000fpnpIO7X",
    publicKeyProduction: "pk_live_51KYzCJK7hYD3CgEU19jZ1b9bVkw0kKkxSL2OtZm8QkJvAP2PadtYmBDZV3PGAZ6GARRirWes0cf7Kyxr52sYI8zv00E1MQxtPY",
  },
  
  google: {
    storage: {
      ...storage,
      publicUrl: `${storage.baseUrl}/${storage.bucket}`
    }
  },

  domain,
  privacyPolicy: domain + "/termos-e-condicoes#politica-de-privacidade",
  termsAndConditions: domain + "/termos-e-condicoes",
  
  whatsApp_CTA_Link: 'https://api.whatsapp.com/send?phone=551151942000'
}