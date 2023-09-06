import { Storage } from '@google-cloud/storage';
import credentials from '../auth/credentials-service-account.json';

import configs from '../../../../configs';

export type AllowedDocumentTypes = "image/png" | "image/jpg" | "image/jpeg" | string;

const storage = new Storage({ credentials });
const BUCKET_NAME = configs.google.storage.bucket;

export const upload = async (folder: string, fileName: string, data: Blob | string | any, contentType: AllowedDocumentTypes) => {

  const destination = `${folder}/${fileName}`;

  const file = storage
  .bucket(BUCKET_NAME)
  .file(destination);

  const fileOptions = {
    public: true,
    resumable: false,
    metadata: { contentType },
    validation: false
  }

  const base64EncodedString = data.replace(/^data:\w+\/\w+;base64,/, '')
  const fileBuffer = Buffer.from(base64EncodedString, 'base64')

  await file.save(fileBuffer, fileOptions);
  
  const completeUrl = `${configs.google.storage.publicUrl}/${destination}`

  // const [metadata] = await file.getMetadata();
  
  return { completeUrl, destination }
}

export const deleteFile = async (folderAndFile: string) => {
  const file = storage
  .bucket(BUCKET_NAME)
  .file(folderAndFile);

  const response = await file.delete()
  .then((response: any) => ({ success: "Arquivo excluído com sucesso!" }))
  .catch((response: any) => {
    if(response.errors[0]) console.log({ error: response.errors[0].message });
    return { error: "Erro ao excluir o documento!" }
  });

  return response;
}