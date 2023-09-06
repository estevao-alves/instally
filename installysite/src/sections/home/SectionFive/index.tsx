import { Button, Container } from "@/styles/layout";
import { styled } from "styled-components";

const Wrapper = styled.div`
  padding: 80px 0 200px;

  @media (max-width: 991px) {
    padding: 60px 0;
  }

  @media (max-width: 576px) {
    padding: 40px 0;
  }
`;

const Content = styled.div`
  display: flex;
  align-items: center;
  position: relative;

  .text {
    h2 {
      max-width: 960px;
      font-size: 55px;
      line-height: 1.3;
    }

    p {
      max-width: 440px;
      font-size: 26px;
      font-weight: 500px;
      margin: 80px 0;
    }

  }

  img {
    width: 100%;
    max-width: 600px;
    position: absolute;
    bottom: -100px;
    right: 0;
  }

  @media (max-width: 1400px) {
    .text {
      h2 {
        max-width: 860px;
        font-size: 46px;
      }

      p {
        font-size: 22px;
        margin: 40px 0;
      }
    }
  }

  @media (max-width: 1200px) {
    img {
      max-width: 500px;
    }
  }

  @media (max-width: 1100px) {
    img {
      max-width: 460px;
    }
  }

  @media (max-width: 991px) {
    flex-direction: column;


    img {
      position: relative;
      bottom: 0;
      width: 100%;
      max-width: 500px;
      margin-top: 40px;
    }
  }

  @media (max-width: 768px) {
    .text {
      h2 {
        max-width: 800px;
        font-size: 40px;
      }

      p {
        font-size: 20px;
        margin: 30px 0 40px;
      }
    }
  }

  @media (max-width: 420px) {
    .text {
      h2 {
        max-width: 380px;
        font-size: 36px;
      }

      p {
        font-size: 18px;
        margin: 20px 0 30px;
      }
    }
  }

  @media (max-width: 380px) {
    .text {
      h2 {
        max-width: 320px;
        font-size: 32px;
      }
    }
  }

`;

export default function SectionFive({ goToAction }: any) {
  return <Wrapper>
    <Container>
      <Content>
        <div className="text">
          <h2>Páginas criadas para se adaptar em qualquer dispositivo</h2>
          <p>Capture a atenção das pessoas que estão buscando pelo seu produto ou serviço na internet.</p>
          <Button onClick={() => goToAction()}>Quero a minha página</Button>
        </div>
        <img src="./images/home-section-six.png" />
      </Content>
    </Container>
  </Wrapper>
}