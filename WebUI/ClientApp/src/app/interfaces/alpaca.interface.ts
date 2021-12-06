import { IFarm } from "./farm.interface";

export interface IAlpaca {
  id?: number,
  name?: string,
  weight?: number,
  color?: string,
  farm?: IFarm;

  cost?: number;

  isSelected?: boolean;
}
