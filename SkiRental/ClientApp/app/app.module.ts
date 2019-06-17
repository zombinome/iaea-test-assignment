import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { SkiListComponent } from './components/skiList/skiList.component'
import { RegisterSkiComponent } from './components/registerSki/registerSki.component';

@NgModule({
  declarations: [
      AppComponent,
      SkiListComponent,
      RegisterSkiComponent
  ],
  imports: [
      BrowserModule,
      FormsModule,
      HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
