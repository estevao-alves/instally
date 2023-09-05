import termos from "./termos.json";

import { Wrapper, Content } from "./styles";
import { Container } from "@/styles/layout";
import LogoSVG from "public/logo.svg";
import HTMLReactParser from "html-react-parser";

export default function TermsAndConditionsSection() {

  return <Wrapper>
    <Container>
      <Content>
        <LogoSVG className="logo" />
      
        <h1>Termos e condições</h1>
        
        {termos?.map((item: { id?: string, title: string, paragraphs: string[] }, i: number) => (
          <div key={i} id={item?.id} className="item">
            <h2>{item.title}</h2>
            {item?.paragraphs?.map((paragraph: string, iParagraph: number) => {
            
              let text = paragraph;

              text = text?.replace("CriarPagina.com", "<span>CriarPagina.com</span>");
              text = text?.replace("você", "<span>você</span>");
            
              return <p key={iParagraph}>{HTMLReactParser(text)}</p>
            })}
          </div>
        ))}

      </Content>
    </Container>
  </Wrapper>
}