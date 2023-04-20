import { Component } from '@angular/core';
import { latLng, tileLayer, Map, Marker } from 'leaflet';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.css']
})
export class ContactComponent {
  options = {
    layers: [tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', { maxZoom: 22, attribution: 'OpenStreetMap' })],
    zoom: 15,
    center: latLng(45.25849, 19.84205)
  };

  layers = [];

  onMapReady(map: Map) {
    const marker = new Marker([45.25849, 19.84205]).addTo(map);
  }
}