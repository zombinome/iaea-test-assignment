var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Injectable } from "@angular/core";
import { map } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
var SkiService = /** @class */ (function () {
    function SkiService(httpClient) {
        this.httpClient = httpClient;
        this.skis = [];
    }
    SkiService.prototype.getSkis = function () {
        var _this = this;
        var observable = this.httpClient
            .get('/api/skis')
            .pipe(map(function (response, _) {
            return response.result.map(skiMapper);
        }));
        observable.subscribe(function (values) { _this.skis = values; });
        return observable;
    };
    SkiService.prototype.registerSki = function (name, rate) {
        var _this = this;
        var observable = this.httpClient
            .post('/api/skis', { name: name, rate: rate })
            .pipe(map(function (response) { return skiMapper(response.result); }));
        observable.subscribe(function (ski) { return _this.skis.push(ski); });
        return observable;
    };
    SkiService.prototype.rentSki = function (id, customerName) {
        var _this = this;
        var observable = this.httpClient
            .post('/api/rent/' + id.toString(), { customerName: customerName })
            .pipe(map(function (response) { return skiMapper(response.result); }));
        observable.subscribe(function (rentedSki) {
            var skiToUpdate = _this.skis.find(function (x) { return x.id === id; });
            if (skiToUpdate == null) {
                _this.skis.push(rentedSki);
            }
            else {
                skiToUpdate.rentTime = rentedSki.rentTime;
                skiToUpdate.customerName = rentedSki.customerName;
            }
        });
        return observable;
    };
    SkiService.prototype.returnSki = function (id) {
        return null;
    };
    SkiService = __decorate([
        Injectable({
            providedIn: 'root'
        }),
        __metadata("design:paramtypes", [HttpClient])
    ], SkiService);
    return SkiService;
}());
export { SkiService };
function skiMapper(dataItem) {
    return new Ski(dataItem.id, dataItem.name, dataItem.rate, dataItem.rentTime ? new Date(dataItem.rentTime) : null, dataItem.customerName);
}
var Ski = /** @class */ (function () {
    function Ski(id, name, rate, rentTime, customerName) {
        this.id = id;
        this.name = name;
        this.rate = rate;
        this.rentTime = rentTime;
        this.customerName = customerName;
    }
    return Ski;
}());
export { Ski };
var SkiRentCost = /** @class */ (function () {
    function SkiRentCost() {
    }
    return SkiRentCost;
}());
export { SkiRentCost };
//# sourceMappingURL=skiServicet.js.map