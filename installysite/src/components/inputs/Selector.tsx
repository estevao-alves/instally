import { styled } from "styled-components";
import ArrowSvg from "@/assets/icons/arrow-to-bottom.svg";
import { useEffect, useState } from "react";

const Wrapper = styled.div`
  margin: 10px 0;

  user-select: none;
  -o-user-select: none;
  -moz-user-select: none;
  -webkit-user-select: none;
  cursor: pointer;

  .option-selected {
    background: rgb(229 229 229);
    border-radius: 10px;
    padding: 15px 25px 15px;
    font-weight: 600;

    display: flex;
    align-items: center;
    justify-content: space-between;

    .option {
      margin: 0 20px 0 0;
    }

    svg {
      --size: 20px;
      width: var(--size);
      min-width: var(--size);
      height: var(--size);

      path {}
    }

    &.active {
      background: rgb(229 229 229);

      svg {
        transform: rotate(180deg);
      }
    }
  }

  .options {
    margin-top: 4px;
    border-radius: 8px;
    overflow: hidden;
    border: 1px solid rgba(0,0,0,.15);
    background-color: rgb(229 229 229);

    div {
      padding: 10px 15px;
      border-bottom: 1px solid rgba(0,0,0,.1);

      &:hover {
        background: rgba(0,0,0,.05);
        border-color: rgba(0,0,0,.05);
      }

      &:last-child {
        border: none;
      }
    }
  }
`;

export default function InputSelector({ options, cb }: { options: string[], cb: (newOption: string) => void }) {
  
  const [optionSelected, setOptionSelected] = useState<string | null>("Selecione uma opção");
  const [opened, setOpened] = useState(false);

  function onChangeOption(newOption: string) {
    setOptionSelected(newOption);
    cb(newOption);
  }
  
  return <Wrapper onClick={() => setOpened(!opened)}>
    <div className={`option-selected ${opened ? "active" : ""}`}>
      <div className="option">{optionSelected}</div>
      <ArrowSvg />
    </div>
    { opened ? 
      <div className="options">
        {options?.filter(option => option !== optionSelected)?.map((option, i) => <div key={i} onClick={() => onChangeOption(option)}>{option}</div>)}
      </div>
    : <></> }
  </Wrapper>
}