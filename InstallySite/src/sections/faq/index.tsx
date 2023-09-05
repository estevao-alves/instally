'use client';

import { useState } from "react";
import Parser from "html-react-parser";
import styled from "styled-components";
import { Container } from "@/styles/layout";

const Section = styled.div`
  padding: 100px 0;
  min-height: 0;
  position: relative;
  overflow: initial;
  z-index: initial;

  @media (max-width: 991px) {
    padding: 60px 0;
  }
`;

const Item = styled.div`
  display: flex;
  flex-direction: column;
  width: 100%;
`;

const TextWrapper = styled.div`
  margin: 0 0 30px;
  text-align: center;

  display: flex;
  flex-direction: column;
  align-items: center;

  position: relative;
  z-index: 2;

  max-width: 100%;
`;

const Title = styled.h3`
  font-size: 40px;
  font-weight: 900;
  line-height: 1.1;
  margin: 0;

  @media (max-width: 991px) {
    font-size: 34px;
  }

  @media (max-width: 576px) {
    font-size: 30px;
  }
`;

const Paragraph = styled.p`
  margin: 0 0 10px;
  font-size: 18px;
  margin: 8px 0 0;

  @media (max-width: 1080px) {
    font-size: 16px;
  }
`;

const Question = styled.div<{ active?: boolean }>`
  padding: 15px 0;

  width: 100%;
  max-width: 800px;
  margin: 0 auto;

  border-bottom: 1px solid rgba(0,0,0,.2);

  h2 {
    font-weight: 700;
    padding: 10px 80px 10px 0;
    position: relative;
    cursor: pointer;
  }

  .response {
    display: ${props => props.active ? 'flex' : 'none'};
  }

  :last-of-type { border: none; }

  @media (max-width: 991px) {
    h2 {
      font-size: 18px;
    }
  }
`;

const ShowMore = styled.div<{ active?: boolean }>`
  width: 40px;
  height: 40px;

  position: absolute;
  top: calc(50% - 20px);
  right: 0;

  cursor: pointer;
  transition: .2s ease-in-out;
  
  &:hover {
    &::before, &::after {
      background: #2b2b2b;
    }
  }

  &::before, &::after {
    content: "";
    width: 60%;
    height: 4px;
    border-radius: 10px;
    background: ${props => props.active ? '#2b2b2b' : '#a1a1a1'};

    position: absolute;
    top: calc(50% - 2px);
    left: 20%;
    transition: .2s ease-in-out;
  }

  &::after {
    transform: ${props => props.active ? 'rotate(0)' : 'rotate(90deg)'};
  }
`;

const ResponseWrapper = styled.div`
  margin: 10px 0 0;
  width: 100%;
  max-width: 700px;
  color: rgba(0,0,0);
  
  display: flex;
  flex-direction: column;
  
  h3 {
    font-size: 18px;
    font-weight: 500;
    margin: 0 0 10px;
  }

  span {
    color: rgba(0,0,0,.8);
    font-weight: 700;
  }

  a {
    color: #1a1a1a;
    font-weight: 700;
    text-decoration: none;

    :hover {
      text-decoration: underline;
    }
  }
  
  ul {
    padding: 0 0 0 20px;

    li {
      margin: 16px 0;
    }
  }

  @media (max-width: 991px) {
    h3 {
      font-size: 16px;
    }
  }
`;

export default function FaqSection({ questions }: { questions: Array<{ title: string, response: string }> }) {

  const [questionActive, setQuestionActive] = useState(0);

  return <Section>
    <Container>
      <Item>
        <TextWrapper>
          <Title>Dúvidas Frequentes</Title>
          <Paragraph>As principais dúvidas</Paragraph>
        </TextWrapper>

        <Item>
          {questions?.map((question: any, i: number) => <Question key={i} active={questionActive === i+1 ? true : undefined}>
            <h2 onClick={() => setQuestionActive(questionActive === i+1 ? 0 : i+1)}>{question?.title} <ShowMore active={questionActive === i+1 ? true : undefined} /></h2>
            <ResponseWrapper className="response">{Parser(String(question?.response))}</ResponseWrapper>
          </Question>)}
        </Item>
      </Item>
    </Container>
  </Section>
}