'use client'

import StyledComponentsRegistry from "@/styles/stylesRegistry";
import GlobalStyles from "@/styles/globals";
import Footer from "@/components/Layout/Footer";
import TermsAndConditionsSection from "@/sections/docs/Termos";

export default function FormularioSucesso() {
  return (
    <StyledComponentsRegistry>

      <TermsAndConditionsSection />

      <Footer />
      <GlobalStyles />
    </StyledComponentsRegistry>
  )
}