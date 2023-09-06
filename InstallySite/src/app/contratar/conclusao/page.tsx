'use client'

import StyledComponentsRegistry from "@/styles/stylesRegistry";
import GlobalStyles from "@/styles/globals";
import CheckoutConclusion from "@/sections/checkout/Conclusion";

export default function ContratarConclusao() {
  return (
      <StyledComponentsRegistry>
        <CheckoutConclusion />
        <GlobalStyles />
      </StyledComponentsRegistry>
    
  )
}
