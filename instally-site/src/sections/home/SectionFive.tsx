import ActionButton from "@/components/actionButton"
import { Container } from "@/styles/layout"
import { styled } from "styled-components"

const Wrapper = styled.div`
  background-image: url("/appsDropping.png");
  background-color: var(--dark-gray);
  background-repeat: no-repeat;
  background-position: 50% -200%;

  padding: 120px 0;

  h2 {
    color: var(--white);
    width: 680px;
    margin: 0 auto 60px auto;

    font-size: 60px;
    font-weight: 800;
    text-align: center;

    span {
      color: var(--purple-simple);
    }
  }
`;


export default function SectionOne() {
  return <Wrapper>
    <Container>
        <h2>Simplifique, <span>Instally</span> e aproveite!</h2>
        <ActionButton />
    </Container>
  </Wrapper>
};