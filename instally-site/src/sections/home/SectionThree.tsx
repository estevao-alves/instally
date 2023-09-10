import ActionButton from "@/components/actionButton"
import { Container } from "@/styles/layout"
import { styled } from "styled-components"

import GirlMeditatingSvg from "@/sections/assets/girlMeditating.svg"

const Wrapper = styled.div`
  background-color: var(--purple-violet);
  padding: 180px 0;
  
  .text {
    max-width: 450px;
    gap: 50px;

    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: baseline;

    h2 {
      color: var(--dark-gray);
      font-size: 70px;
      font-weight: 800;
    }

    p {
      max-width: 400px;
      font-size: 24px;
    }
  }

  .girlMeditationg {
    width: 820px;
    height: 580px;
  }
`;

const Content = styled.div`
  display: flex;
  align-items: center;
  margin-right: -300px;
`;

export default function SectionOne() {
  return <Wrapper>
    <Container>
      <Content>
        <GirlMeditatingSvg className="girlMeditationg" />
        <div className="text">
          <h2>Seus apps <span>do seu jeito!</span></h2>
          <p>Mantenha seus apps organizados, atualizados sem esforço e sempre em mãos.</p>
          <ActionButton text={"Baixar Agora"} disableIcon={true} style={{backgroundColor: "#000", padding: "20px 80px"}} />
        </div>
      </Content>
    </Container>
  </Wrapper>
};