import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { error } from '@angular/compiler/src/util';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  values: any;
  constructor(private http: HttpClient) { }
  ngOnInit()
  {
    this.getvalues();
  }

  getvalues() {
    this.http.get('https://localhost:44313/api/values').subscribe(Response => {
      this.values = Response;
    }, error => {
        console.log(error);
    })
  }
}
