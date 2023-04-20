import { TermSimple } from "./Term";

export interface BarberService {
    id: number;
    name: string;
    duration: number;
    img: string;
    terms: TermSimple[]
}
