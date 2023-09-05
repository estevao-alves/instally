'use client'

import StyledComponentsRegistry from "@/styles/stylesRegistry";
import GlobalStyles from "@/styles/globals";
import CheckoutSection from "@/sections/checkout";

export default function Contratar() {
  return (
    <StyledComponentsRegistry>
      <CheckoutSection />
      <GlobalStyles />
    </StyledComponentsRegistry>
  )
}
