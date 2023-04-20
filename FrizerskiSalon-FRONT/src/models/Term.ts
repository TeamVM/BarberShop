import { Time } from "@angular/common";

export interface Term {
    termID: number;
    barberShopID: number;
    dateTime: Date;
    startTime: string;
    endTime: string;
    userID: number;
    serviceID: number;
    busy: boolean;
    workerID: number;
}

export interface TermSimple {
    booked: boolean;
    startTime: Date; // datum i vreme pocetka
    duration: number; //trajanje termina u minutima
}
