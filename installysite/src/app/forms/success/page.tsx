'use client'

import StyledComponentsRegistry from "@/styles/stylesRegistry";
import GlobalStyles from "@/styles/globals";
import Footer from "@/components/Layout/Footer";
import FormSuccessSection from "@/sections/forms/success";

export default function FormularioSucesso() {
  return (
    <StyledComponentsRegistry>

      <FormSuccessSection />

      <Footer />
      <GlobalStyles />
    </StyledComponentsRegistry>
  )
}