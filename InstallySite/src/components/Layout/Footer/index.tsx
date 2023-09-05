import styled from "styled-components";

import SaturniaLogo from './assets/saturnia-logo.svg';
import BrasilIcon from './assets/saturnia-brazil-flag.svg';
import { Container } from "@/styles/layout";
import configs from "../../../../configs";

const Wrapper = styled.div`
  padding: 80px 0;
  background: #fff;
  position: relative;

  .logo, .logo path {
    fill: rgba(0,0,0,.7);
  }

  > div, > div > div > div {
    display: flex;
    flex-direction: column;
    align-items: center;
    text-align: center;
  }
`;

const BusinessAndLinks = styled.div`

  @media (max-width: 680px) {
    > div:first-child {
      justify-content: flex-start;
      align-items: flex-start;
      text-align: initial;
    }

    > div:last-child {
      display: flex;
      flex-direction: column;
    }
  }

  h3.business {
    font-size: 16px;
    margin: 30px 0 20px;
  }

  h3 {
    font-size: 14px;
    font-weight: 500;
    margin: 0 0 10px;
  }

  p {
    font-size: 14px!important;
  }
`;

const Copyright = styled.div`
  margin: 60px 0 0;

  p {
    font-size: 12px!important;
    margin: 0!important;
  }
`;

const Paragraph = styled.p<{ fontSize?: string, margin?: string, textAlign?: string }>`
  margin: 0 0 10px;
  font-size: ${props => props.fontSize || '20px'};
  margin: ${props => props.margin};
  text-align: ${props => props.textAlign};

  @media (max-width: 1080px) {
    font-size: 16px;
  }
`;

const Item = styled.div<{ display?: string, flexDirection?: string, alignItems?: string, justifyContent?: string, textAlign?: string, padding?: string, margin?: string, responsive991?: string }>`
  display: ${props => props.display || 'flex'};
  flex-direction: ${props => props.flexDirection || 'column'};
  align-items: ${props => props.alignItems || 'flex-start'};
  justify-content: ${props => props.justifyContent || 'flex-start'};
  text-align: ${props => props.textAlign || 'start'};
  padding: ${props => props.padding || '0'};
  margin: ${props => props.margin || '0'};
  width: 100%;

  @media (max-width: 991px) {
    ${props => props.responsive991};
  };

  h3 > a {
    font-weight: 500;
    text-decoration: none;
    color: black!important;
  }
`;

const Links = styled.div`
  margin: 40px 0 0;

  display: flex;
  flex-direction: row!important;

  @media (max-width: 480px) {
    margin: 20px 0 0;
    flex-direction: column!important;
    align-items: flex-start!important;
  }
`;

const Link = styled.a<{ margin?: string, fontSize?: string }>`
  margin: 0 20px;
  font-size: 14px;
  font-weight: 600;
  color: var(--green);

  text-decoration: none;
  
  :hover {
    text-decoration: underline;
  }

  @media (max-width: 480px) {
    margin: 10px 0;
  }
`;

export default function Footer() {
  return <Wrapper>
    <Container>
      <BusinessAndLinks>
        <Item>
          <SaturniaLogo className="logo" height={24} />
          <h3 className="business">Saturnia Tecnologia LTDA.</h3>
          <Paragraph fontSize="14px">Avenida Paulista, 1636 - Sala 1504 <br/>Edif. Paulista Corporate, São Paulo, Brasil</Paragraph>
          <Paragraph fontSize="14px">contato@saturniatecnologia.com.br</Paragraph>
          <Paragraph fontSize="14px"><BrasilIcon width={20} style={{ margin: '0 4px -2px 0'}} /> +55 (11) 5194-2000</Paragraph>
        </Item>

        <Links>
          <Link target="_blank" href={configs.termsAndConditions}>Termos e condições</Link>
          <Link target="_blank" href={configs.privacyPolicy}>Política de privacidade</Link>
        </Links>

      </BusinessAndLinks>
      <Copyright>
        <Paragraph>Copyright © 2023 Saturnia. Todos os direitos reservados.</Paragraph>
      </Copyright>
    </Container>
  </Wrapper>
}