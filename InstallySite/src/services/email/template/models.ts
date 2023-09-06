const emailTemplates = [

  {
    title: "Boas-vindas",
    subject: `Recebemos seu contato!`,
    message: `
      <span style="font-weight:bold;">Boas-vindas, {{firstName}}!</span><br/><br/>

      Nossos especialistas já receberam suas informações e deverão entrar em contato com você o mais rápido possível.<br/><br/>

      Se você não quer esperar, é só clicar no botão abaixo para falar com a gente agora mesmo 😊.
    `,
    button: {
      text: "Chamar no WhatsApp",
      url: ""
    }
  },

]

export default emailTemplates;