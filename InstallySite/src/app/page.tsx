'use client'

import StyledComponentsRegistry from "@/styles/stylesRegistry";
import GlobalStyles from "@/styles/globals";

// Page sections
import SectionOne from "@/sections/home/SectionOne";
import SectionTwo from "@/sections/home/SectionTwo";
import SectionThree from "@/sections/home/SectionThree";
import SectionFour from "@/sections/home/SectionFour";
import SectionFive from "@/sections/home/SectionFive";
import Reviews from "@/sections/home/Reviews";
import Pricing from "@/sections/home/Pricing";
import FaqSection from "@/sections/faq";
import Footer from "@/components/Layout/Footer";

const questions = [
  { title: "O que é uma landing page?", response: "Uma landing page é uma página da web projetada com foco específico em converter visitantes em leads ou clientes, geralmente através de uma única oferta ou chamada para ação.  Um botão com um único destino para conversão." },
  { title: "Como uma página pode impulsionar meu negócio?", response: "Uma landing page bem projetada pode impulsionar seu negócio de várias maneiras, incluindo a geração de leads, o foco em campanhas específicas, lançamentos e venda de algum produto. Ela se concentra em uma oferta única, aumenta a confiança dos visitantes e direciona a ação desejada, resultando em crescimento e sucesso para sua empresa." },
  { title: "Em quanto tempo minha página fica pronta?", response: "O prazo estimado desde a contratação até a entrega de um projeto de página varia geralmente de 05 a 07 dias úteis, considerando, é claro, as particularidades do projeto, já que cada um apresenta suas próprias demandas e requisitos únicos." },
  { title: "Vou poder fazer alterações futuras em minha página?", response: "No plano mensal, oferecemos suporte técnico completo para fornecer assistência contínua e garantir atualizações regulares em sua página." },
  { title: "Minha página vai funcionar em celulares, tablets e outros dispositivos?", response: "Sim, toda página criada é otimizada, o que garante que os visitantes tenham uma boa experiência em smartphones e tablets, melhorando as chances de conversão." },
  { title: "O que devo fazer após criar minha landing page?", response: "Promova sua landing page através de canais relevantes, como redes sociais, campanhas de e-mail marketing e anúncios pagos. Monitore o desempenho e faça ajustes conforme necessário para melhorar as conversões." }
]

function goToAction() {
  const sectionPricing = document.getElementById("pricing");
  sectionPricing?.scrollIntoView({ behavior: "smooth", block: "start" });
  
  //window.open("https://api.whatsapp.com/send?phone=551151942000");
}

export default function Home() {
  return (
    <StyledComponentsRegistry>
      <main>
        <SectionOne goToAction={goToAction} />
        <SectionTwo goToAction={goToAction} />
        <SectionThree goToAction={goToAction} />
        <SectionFour goToAction={goToAction} />
        <SectionFive goToAction={goToAction} />
        <Pricing />
        <Reviews goToAction={goToAction} />
        <FaqSection questions={questions} />
        <Footer />
      </main>
      <GlobalStyles />
    </StyledComponentsRegistry>
  )
}
