import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ButtonSampleComponent } from "../../../ui/src/lib/components/button-sample/button-sample.component";
import { HttpClient } from '@angular/common/http';
import { JsonPipe } from '@angular/common';

@Component({
  selector: 'vaa-root',
  imports: [RouterOutlet, ButtonSampleComponent, ButtonSampleComponent, JsonPipe],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit{
  title = 'web';
  httpClient = inject(HttpClient);

  res: any;

  ngOnInit(): void {
    this.httpClient.get("https://localhost:7276/api/v1/health").subscribe(r => this.res = r);
  }
}
