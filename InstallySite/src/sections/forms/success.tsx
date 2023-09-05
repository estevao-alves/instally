import { Wrapper, Content } from "./styles";
import { Container } from "@/styles/layout";
import LogoSVG from "public/logo.svg";

export default function FormSuccessSection() {

  return <Wrapper>
    <Container>
      <Content>
        <LogoSVG className="logo" />
      
        <div className="text">
          <h1>Tudo certo, obrigado!</h1>
          <h2>As informações foram enviadas corretamente.</h2>

          <p style={{ maxWidth: 500 }}>Em caso de dúvidas ou dificuldades por favor, entre em contato com a nossa equipe de atendimento.</p>
        </div>

      </Content>
    </Container>
  </Wrapper>
}