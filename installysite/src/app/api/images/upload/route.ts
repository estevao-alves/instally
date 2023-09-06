import { NextResponse } from 'next/server';
import googleServices from '@/services/google';

export async function POST(request: Request) {

  const formData = await request.formData();
  
  const clientPathName = (await formData).get("email") as string;
  const logo = (await formData).getAll("logo[]") as File[];
  const imagens = (await formData).getAll("imagens[]") as File[];

  // Verify Token
  // const auth = new Auth(request);
  // if(!auth.isValid() || !auth.user._id) return NextResponse.json({ error: "Você não tem acesso!" });

  const logosUploaded = await Promise.all(logo?.map(async (file) => {
    const fileData = await file.arrayBuffer();
    const base64Data = Buffer.from(fileData).toString('base64');
    var { destination } = await googleServices.storage.upload(clientPathName, file.name, base64Data, file.type);
    return destination;
  }));
  
  const imagesUploaded = await Promise.all(imagens?.map(async (file) => {
    const fileData = await file.arrayBuffer();
    const base64Data = Buffer.from(fileData).toString('base64');
    var { destination } = await googleServices.storage.upload(clientPathName, file.name, base64Data, file.type);
    return destination;
  }));

  console.log(imagesUploaded)

  return NextResponse.json({ logo: logosUploaded, images: imagesUploaded });
}