import { Component, OnInit } from '@angular/core';
import {ServiceService} from "../service.service";
import {Router} from "@angular/router";
import {max} from "rxjs/operators";

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent implements OnInit {

  elements: {id: number, destination: string, country: string, price: number}[];
  elementsMap: Map<number, {id: number, destination: string, country: string, price: number}> = new Map<number, {id: number; destination: string; country: string; price: number}>()
  constructor(private service: ServiceService) { }
  isLogged = false;
  user = "";
  mapAsc: Map<number, number>;
  sorted: {}
  apparition: Map<number, number> = new Map<number, number>();
  mostPopular: {id: number, destination: string, country: string, price: number}[] = [];
  ngOnInit(): void {
    this.service.getAll().subscribe(
      result => {
        console.log(result);
      }
    )
  }

  login(username: string) {
    console.log(username);
    this.user = username;
    this.service.login(username).subscribe(
      result => {
        if (result["type"] === "success") {
          this.isLogged = true;
        }
      }
    )
  }

  addBanned(destinationId: number) {
    this.service.addBanned(destinationId, this.user).subscribe();
  }

  showMostPopular() {
    this.mostPopular = [];
    let arr = []
    new Map([...this.apparition.entries()].sort((a, b) => b[1] - a[1])).forEach((key, el) => arr.push(el));
    console.log(arr);
    if (arr.length > 3) {
      for (let i of Array(3).keys()) {
        this.mostPopular.push(this.elementsMap.get(arr[i]))
      }
    } else {
      for (let i of Array(arr.length).keys()) {
        this.mostPopular.push(this.elementsMap.get(arr[i]))
      }
    }

  }

  search(destionation: string) {
    this.service.search(destionation, this.user).subscribe(
      result => {
        this.elements = result["data"];
        console.log(this.elements);
        for (let elem of this.elements) {
          if (this.apparition.get(elem.id) === null || this.apparition.get(elem.id) === undefined)
            this.apparition.set(elem.id, 1);
          else {
            this.apparition.set(elem.id, this.apparition.get(elem.id) + 1);
          }
          this.elementsMap.set(elem.id, elem);
        }
      }
    )
  }
}
