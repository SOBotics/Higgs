import { Injectable } from '@angular/core';
import { Subject } from 'rxjs/Subject';
import { Observable } from 'rxjs/Observable';

export interface HttpInterception {
  type: 'readystatechangeevent'; // To be expanded when we capture more requests
  request: XMLHttpRequest;
}

@Injectable()
export class HttpRequestInterceptorService {
  static readyStateChangeSubject;
  static sentSubject;
  constructor() {
    if (HttpRequestInterceptorService.readyStateChangeSubject) {
      return;
    }
    HttpRequestInterceptorService.readyStateChangeSubject = new Subject<HttpInterception>();
    HttpRequestInterceptorService.sentSubject = new Subject<XMLHttpRequest>();

    const originalSend = XMLHttpRequest.prototype.send;
    XMLHttpRequest.prototype.send = function sendReplacement(data) {
      HttpRequestInterceptorService.sentSubject.next(this);
      this.addEventListener('readystatechange', function (event) {
        HttpRequestInterceptorService.readyStateChangeSubject.next({ type: 'readystatechangeevent', request: event.target });
      });
      return originalSend.apply(this, arguments);
    };
  }

  public ReadyStateChangedObservable(): Observable<HttpInterception> {
    return HttpRequestInterceptorService.readyStateChangeSubject;
  }
  public SentObservable() {
    return HttpRequestInterceptorService.sentSubject;
  }
}
