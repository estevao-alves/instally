import { mailTemplate } from './template';
import googleServices from '../google';

type PersonType = {
  email: string;
  name?: string;
}

type SendMailTypes = {
  from?: PersonType;
  to?: Array<PersonType>;
  subject?: string;
  text?: string;
  message: string;
  button?: {
    text: string;
    url: string;
  }
};

export async function sendMail({ from = {
    email: 'contato@saturniatecnologia.com.br',
    name: 'CriarPagina.com'
  },
  to = [
    { name: "Gabriel Afonso", email: "gabrielafonsome@gmail.com" }
  ],
  subject = '',
  text = '',
  message = '',
  button
  }: SendMailTypes) {

  const html = mailTemplate({ message, button })
  
  await googleServices.gmail.send({ from, to, subject, text, html })
}