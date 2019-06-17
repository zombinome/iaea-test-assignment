import { Component, OnInit } from '@angular/core';
import { SkiService, Ski } from './skiService';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
  //styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
    loading = false;
    skis: Ski[] = [];

    constructor(public skiService: SkiService) {
    }

    ngOnInit() {
        this.loading = true;
        this.skiService.getSkis().subscribe((skis) => {
            this.loading = false;
            this.skis = skis;
        })
    }


    title = 'Welcome to Angular';
    subtitle = '.NET Core + Angular CLI v7 + Bootstrap & FontAwesome + Swagger Template';
}
