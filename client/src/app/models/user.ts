import { Stock } from "./stock";

export interface User {
    id: string;
    firstName: string;
    lastName: string;
    email: string;
    password: string;
    stocks: Stock[];
  }