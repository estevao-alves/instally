import { useRef, useState } from "react";
import { SliderPicker } from "react-color";
import styled from "styled-components"

const Wrapper = styled.div`
  display: flex;
  justify-content: space-between;
  align-items: center;

  > div {
    width: 100%;

    &.slider-picker {
      > div:nth-of-type(2) > div {
        margin-top: 10px!important;
      }
    }
  }
`;

const Content = styled.div`
  display: flex;
  align-items: center;

  border: 1px solid rgba(0,0,0,.3);
  padding: 8px;
  width: 100%;
  max-width: 150px;
  border-radius: 10px;
  cursor: pointer;
  margin-left: 20px;
`;

const Color = styled.div<{ color: string }>`
  min-width: 30px;
  max-width: 30px;
  height: 30px;
  border-radius: 6px;
  background: ${props => props.color};
  margin: 0 10px 0 0;
  border: 1px solid rgba(0,0,0,.3);
`;

const Input = styled.input`
  max-width: 70px;
  border: none;
  background: transparent;
  margin: 0 5px;

  font-weight: 600;
  text-transform: uppercase;
  opacity: .8;
`;

export default function InputHex({ color, setColor }: any) {
  const inputRef = useRef(null) as any;

  return <Wrapper>
    <SliderPicker color={color} onChange={({ hex }) => setColor(hex)} />
    <Content onClick={() => inputRef?.current.focus()}>
      <Color color={`#${color.split("#").join("")}`} />
      <div>#</div>
      <Input ref={inputRef} maxLength={6} type="text" placeholder="HEX" value={color === "transparent" ? "" : color.split("#").join("")} onChange={(e: any) => setColor(`#${e.target.value?.toUpperCase()}`)} />
    </Content>
  </Wrapper>
}