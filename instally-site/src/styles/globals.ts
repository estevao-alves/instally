'use client'

import { createGlobalStyle } from "styled-components";

const GlobalStyle = createGlobalStyle`
  * {
    padding: 0;
    margin: 0;
    box-sizing: border-box;
  }

  :root {
    --purple-violet: #6247AA;
    --purple-simple: #7251B5;

    --gray: #DFDFDF;
    --white: #FFF;

    --dark-gray: #1A1A1A;
    --black: #000;
  }

  h1, h2, h3, h4, p, span {
    color: var(--white);
  }

  button, input, textarea {
    font-family: var(--font-family);
  }

  button {
    border: none;
    cursor: pointer;
  }

  button.cta {
    background-color: var(--purple-violet);
    color: var(--white);

    padding: 14px 38px;
    display: flex;
    
    border-radius: 40px;
    align-self: center;
    font-size: 22px;
    font-weight: bold;
    text-transform: uppercase;

    transition: 200ms ease-in-out;

    &:hover {
      transform: scale(1.02, 1.02);
      background-color: #443176 !important;
    }
  }

  @media (max-width: 576px) {
    button.cta  {
      width: 100%;
      justify-content: center;

      font-size: 20px;
    }

    @media (max-width: 480px) {
      button.cta {
        font-size: 17px;
      }
    }

    @media (max-width: 380px) {
      button.cta {
        padding: 15px 30px;
        font-size: 14px;
      }
    }
  }
`;

export default GlobalStyle;