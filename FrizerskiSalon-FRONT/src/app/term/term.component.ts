import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { BarberService } from 'src/models/Service';
import { TermSimple } from 'src/models/Term';
import { BarberWorker } from 'src/models/Worker';

@Component({
  selector: 'app-term',
  templateUrl: './term.component.html',
  styleUrls: ['./term.component.css']
})
export class TermComponent implements OnInit {

  minDate!: string;
  maxDate!: string;
  selectedDateParent!: Date;
  selectedService!: BarberService;


  public workers: BarberWorker[] = [
    {
      workerID: 1,
      name: 'Boris Debeli',
      img: '../assets/img/sisanje.jpeg',
      terms: []
    },
    {
      workerID: 2,
      name: 'Frizer2',
      img: '../assets/img/sisanje.jpeg',
      terms: []
    }
  ]

  public services: BarberService[] = [
    { id: 1, name: 'Sisanje i brada', duration: 30, img: '../assets/img/sisanje.jpeg', terms: [] },
    { id: 2, name: 'Sisanje', duration: 15, img: '../assets/img/sisanje.jpeg', terms: [] }
  ];
  constructor() {


  }

  ngOnInit(): void {
    const currentDate = new Date();
    const currentDayOfWeek = currentDate.getDay();

    // Postavljanje minimalnog datuma
    let minDate;
    if (currentDayOfWeek === 0) {
      // Ako je trenutni dan nedjelja, postavljamo minimalni datum na ponedjeljak
      const daysUntilMonday = 1;
      const minDateMonday = new Date(currentDate);
      minDateMonday.setDate(currentDate.getDate() + daysUntilMonday);
      minDate = minDateMonday.toISOString().split('T')[0];
    } else {
      // Inače, postavljamo minimalni datum na trenutni dan
      minDate = currentDate.toISOString().split('T')[0];
    }

    // Postavljanje maksimalnog datuma na trenutni dan + broj dana do subote
    const daysUntilSaturday = (6 - currentDayOfWeek) % 7;
    const maxDate = new Date(currentDate);
    maxDate.setDate(currentDate.getDate() + daysUntilSaturday);
    const maxDateString = maxDate.toISOString().split('T')[0];

    this.minDate = minDate;
    this.maxDate = maxDateString;

  }


  addTerms(event: any) {

    this.selectedService.terms = [];

    this.selectedDateParent = event.value;

    const date = new Date(this.selectedDateParent);

    const isSaturday = date.getDay() === 6;

    const openingTime = new Date(date)
    const closingTime = new Date(date);

    openingTime.setHours(10, 0, 0, 0);
    if (isSaturday) {
      closingTime.setHours(15, 0, 0, 0)
    } else {
      closingTime.setHours(19, 0, 0, 0);
    }

    let currentTerm = openingTime;
    closingTime.setMinutes(closingTime.getMinutes() - this.selectedService.duration)
    do {

      const newTerm = new Date(currentTerm);

      newTerm.setMinutes(currentTerm.getMinutes() + this.selectedService.duration);

      // console.log(newTerm);

      this.selectedService.terms.push({
        startTime: newTerm,
        duration: this.selectedService.duration,
        booked: false
      }
      );

      currentTerm = newTerm;

    } while (currentTerm < closingTime)

    console.table(this.selectedService.terms)

  }







}
