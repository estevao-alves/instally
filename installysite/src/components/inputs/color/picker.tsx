import React, { useEffect, useState } from "react";
import { SliderPicker } from "react-color";
import styled from "styled-components";

const Wrapper = styled.div<{ colorselected: string; }>`
  display: flex;
  align-items: center;
  margin: 20px 0;
  
  .colors {
    margin: 20px 0;
    width: 100%;

    display: flex;
    align-items: center;

    .pallet {
      display: grid;
      grid-template-columns: repeat(7, 1fr);
      gap: 6px;
      margin: 0 20px;
    }

    .slider-picker {
      z-index: 9999!important;
      width: 100%;
      cursor: pointer;
      
      .hue-horizontal {
        border-radius: 3px;

        > div > div {
          background-color: ${props => `${props.colorselected}!important`};
          width: 25px!important;
          height: 25px!important;
          transform: translate(-7px,-7px)!important;
          border-radius: 100%!important;
          box-shadow: 0 2px 10px rgba(0,0,0,.3)!important;
          border: 3px solid #fff!important;
        }
      }
    }
  }

  @media (max-width: 768px) {
    flex-direction: column;
    align-items: flex-start;
    
    .colors {
      align-items: flex-start;
      flex-direction: column;
      min-width: initial;

      .pallet {
        margin: -74px 0 20px auto;
      }

      .slider-picker {
        margin: 20px 0;
        width: 100%;
        max-width: calc(100vw - 60px);
      }
    }
  }
`;

const Sample = styled.div<{ color: string; border: number; }>`
  min-width: 24px;
  max-width: 24px;
  height: 24px;
  border-radius: 5px;
  cursor: pointer;
  background: ${props => props.color};

  ${props => props.border === 1 && `border: 1px solid rgba(0,0,0,.3);` };
`;

const SampleMain = styled.div<{ colorselected: string }>`
  min-width: 54px;
  width: 54px;
  height: 54px;
  border-radius: 10px;
  background: ${props=> props?.colorselected || "#FFF"};
  border: 1px solid rgba(0,0,0,.2);

  cursor: pointer;
  transition: .2s ease-in-out;
  opacity: 1;

  :hover {
    opacity: .9;
  }
`;

type PickerTypes = {
  defaultColor: string;
  cb: (value: string) => void;
  className?: string;
}

const pallet = [
  { hex: "#FFF", border: true },
  { hex: "#C9C9C9" },
  { hex: "#939393" },
  { hex: "#5E5E5E" },
  { hex: "#3E3E3E" },
  { hex: "#202020" },
  { hex: "#000" },

  { hex: "#FF6900" },
  { hex: "#FCB900" },
  { hex: "#00D084" },
  { hex: "#2CCCE4" },
  { hex: "#0693E3" },
  { hex: "#EB144C" },
  { hex: "#9900EF" },
]

export default function ColorPicker({ defaultColor, cb, className }: PickerTypes) {
  const [colorSelected, setColorSelected] = useState<string>(defaultColor);

  function handleChange(color: any) {
    setColorSelected(color?.hex)
    cb(color?.hex)
  }

  useEffect(() => handleChange({ hex: defaultColor ? defaultColor : "#FFF" }), [defaultColor])
  
  return (
    <Wrapper colorselected={colorSelected} className={className}>
      <SampleMain colorselected={colorSelected} />
      <div className="colors">
        <div className="pallet">
          {pallet?.map((color, i) => color?.hex && <Sample key={i} color={color?.hex} border={color?.border ? 1 : 0} onClick={() => handleChange(color)} />)}
        </div>
        <SliderPicker color={colorSelected} onChange={handleChange} />
      </div>
    </Wrapper>
  )
}