import { Injectable } from '@angular/core';

@Injectable()
export class MetaDataService {

  constructor() { }

  public setTitle(title: string) {
    document.getElementById('metaData_title').textContent = title;
  }

  public setFavIcon(url: string) {
    document.getElementById('metaData_favIcon').setAttribute('href', url);
  }
}
