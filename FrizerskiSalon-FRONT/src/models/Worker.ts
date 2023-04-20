import { TermSimple } from "./Term";

export interface BarberWorker {
    workerID: number;
    name: string;
    img: string;
    terms: TermSimple[]
}