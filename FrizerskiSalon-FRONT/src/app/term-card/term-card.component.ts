import { Component, OnChanges, OnInit } from '@angular/core';
import { BarberService } from 'src/models/Service';
import { Input } from '@angular/core';
import { BarberWorker } from 'src/models/Worker';
import { Term, TermSimple } from 'src/models/Term';

@Component({
  selector: 'app-term-card',
  templateUrl: './term-card.component.html',
  styleUrls: ['./term-card.component.css']
})
export class TermCardComponent {

  @Input() service!: BarberService;
  @Input() worker!: BarberWorker;
  @Input() selectedDate!: Date;

  bookTerm(term: TermSimple) {
    console.log('rezervisao sam termin!')
  }

}
