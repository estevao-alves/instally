import { NextResponse } from 'next/server';
import { connectToDatabase } from '@/services/database';
import { Auth } from '@/services/auth';

export async function GET(request: Request) {

  // Verify Token
  const auth = new Auth(request);
  if(!auth.isValid()) return NextResponse.json({ error: "Você não tem acesso!" });

  const { db } = await connectToDatabase();
  const formsCollection = db.collection("forms");

  const formsList: any[] | any[] = await formsCollection.find({})?.sort({ createdAt: -1 }).toArray();

  return NextResponse.json(formsList);
}