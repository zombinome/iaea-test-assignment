import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { catchError, map, tap } from 'rxjs/operators';

import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
    providedIn: 'root'
})
export class SkiService {
    constructor(private httpClient: HttpClient) { }

    skis: Ski[] = [];

    getSkis(): Observable<Ski[]> {
        const observable = this.httpClient
            .get('/api/skis')
            .pipe<Ski[], Ski[]>(
                map<any, Ski[]>((response: any, _) => response.result.map(skiMapper)),
                tap(values => {
                    this.skis = values;
                    return values;
                })
            );

        return observable;
    }

    registerSki(name: string, rate: number): Observable<Ski> {
        const observable = this.httpClient
            .post('/api/skis', { name, rate })
            .pipe(
                map((response: any) => skiMapper(response.result)),
                tap((ski: Ski) => {
                    this.skis.push(ski);
                    return ski;
                })
            );

        return observable;
    }

    rentSki(id: number, customerName: string): Observable<Ski> {
        const observable = this.httpClient
            .post('/api/rent/' + id.toString(), { customerName })
            .pipe(
                map((response: any) => skiMapper(response.result)),
                tap((rentedSki) => {
                    const skiToUpdate = this.skis.find(x => x.id === id);
                    if (skiToUpdate == null) {
                        this.skis.push(rentedSki);
                    }
                    else {
                        skiToUpdate.rentTime = rentedSki.rentTime;
                        skiToUpdate.customerName = rentedSki.customerName;
                    }
                    return rentedSki;
                })
            );

        return observable;
    }

    returnSki(id: number): Observable<SkiRentCost> {
        const observable = this.httpClient
            .post(`/api/rent/${id.toString()}/return`, {})
            .pipe(
                map<any, SkiRentCost>(response => skiRentCostMapper(response.result)),
                tap(rentCost => {
                    const skiToUpdate = this.skis.find(x => x.id === id);
                    if (!!skiToUpdate) {
                        skiToUpdate.rentTime = null;
                        skiToUpdate.customerName = null;
                    }

                    return rentCost;
                }));

        return observable;
    }
}

function skiMapper(dataItem: any): Ski {
    return new Ski(
        dataItem.id,
        dataItem.name,
        dataItem.rate,
        dataItem.rentTime ? new Date(dataItem.rentTime) : null,
        dataItem.customerName);
}

function skiRentCostMapper(dataItem: any): SkiRentCost {
    return new SkiRentCost(dataItem.skiId, dataItem.rentCost, dataItem.customerName);
}

export class Ski {
    constructor(
        public id: number,
        public name: string,
        public rate: number,
        public rentTime: Date,
        public customerName: string) { }
}

export class SkiRentCost {
    constructor(
        public skiId: number,
        public rentCost: number,
        public customerName: string) { }
}