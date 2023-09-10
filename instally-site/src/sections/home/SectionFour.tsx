import { Container } from "@/styles/layout"
import { styled } from "styled-components"

import AppExampleSvg from "@/sections/assets/appExample.svg"

const Wrapper = styled.div`
  padding: 120px 0;

  .appExample {
    position: relative;
  }

  h2 {
    color: var(--dark-gray);
    margin-bottom: 60px;

    font-size: 60px;
    font-weight: 800;
    text-align: center;

    span {
      color: var(--purple-violet);
    }
  }
`;

export default function SectionOne() {
  return <Wrapper>
    <Container>
        <h2>Mais eficiência, <span>menos complicação.</span></h2>
        <AppExampleSvg className="appExample" />
    </Container>
  </Wrapper>
};