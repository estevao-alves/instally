import { NextResponse } from "next/server";

const stripe = require("stripe")(process.env.STRIPE_SK_PRODUCTION);

export async function POST(request: Request) {
  const { amount, paymentMethodId } = await request.json();

  if(!amount) return NextResponse.json({ error: { message: "Informe um valor para a cobrança." } })
  if(!paymentMethodId) return NextResponse.json({ error: { message: "Informe o Id do método de pagamento da cobrança." } })

  const paymentIntent = await stripe.paymentIntents.create({
    amount,
    currency: "brl",
    payment_method: paymentMethodId,
    automatic_payment_methods: { enabled: true },
  });

  return NextResponse.json(paymentIntent.client_secret);
}