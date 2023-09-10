import styled from "styled-components";

import DownloadSvg from "public/download.svg"

const Wrapper = styled.div`
  display: flex;
  justify-content: center;

  button {
    align-items: center;
    
    .collection {
      margin-right: 18px;
    }
  }

  .downloadSvg {
    width: 30px;
  }

  @media (max-width: 480px) {
    .downloadSvg {
      width: 25px;
    }
  }
`;

export default function ActionButton({text, disableIcon, style}: any) {
  return <Wrapper>
    <button className="cta" style={style}>
      {disableIcon || <div className="collection"><DownloadSvg className="downloadSvg" /></div> }
      {text || "Baixar para Windows"}
      </button>
  </Wrapper>
}