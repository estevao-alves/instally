'use client'

import StyledComponentsRegistry from "@/styles/stylesRegistry";
import GlobalStyles from "@/styles/globals";
import FormInfoSection from "@/sections/forms";
import Footer from "@/components/Layout/Footer";

export default function Formulario() {
  return (
    <StyledComponentsRegistry>
      <FormInfoSection />
      <Footer />
      <GlobalStyles />
    </StyledComponentsRegistry>
  )
}