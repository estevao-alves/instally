import { google } from 'googleapis';
import MailComposer from 'nodemailer/lib/mail-composer';
import credentials from '../auth/credentials.json';
import tokens from '../auth/tokens.json';

const getGmailService = () => {
  const { client_secret, client_id, redirect_uris } = credentials.web;
  const oAuth2Client = new google.auth.OAuth2(client_id, client_secret, redirect_uris[0]);
  oAuth2Client.setCredentials(tokens);
  const gmail = google.gmail({ version: 'v1', auth: oAuth2Client });
  return gmail;
};

const encodeMessage = (message: any) => {
  return Buffer.from(message).toString('base64').replace(/\+/g, '-').replace(/\//g, '_').replace(/=+$/, '');
};

const createMail = async (options: any) => {
  const mailComposer = new MailComposer(options);
  const message = await mailComposer.compile().build();
  return encodeMessage(message);
};

export const send = async ({ from, to, subject, text, html }: any) => {

  const emails = await Promise.all(to?.map((receiver: { name?: string, email: string }) => receiver?.name ? `${receiver?.name} <${receiver?.email}>` : receiver.email));

  const options = {
    from: from?.name ? `${from.name} <${from?.email}>` : from?.email,
    to: emails,
    replyTo: from.email,
    subject,
    text,
    html,
    textEncoding: 'base64',
    headers: [
      { key: 'X-Application-Developer', value: 'Saturnia' },
      { key: 'X-Application-Version', value: 'v1.0.0' },
    ],
  };

  const gmail = getGmailService() as any;
  const rawMessage = await createMail(options) as any;
  const { data } = await gmail.users.messages.send({
    userId: 'me',
    resource: {
      raw: rawMessage
    }
  });
  return data;
};