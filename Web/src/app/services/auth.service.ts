import { Injectable, ModuleWithComponentFactories } from '@angular/core';
import { Configuration } from '../../swagger-gen';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { Observable } from 'rxjs/Observable';
import { AuthenticationService } from '../../swagger-gen/api/authentication.service';

export const AccessTokenStorageKey = 'access_token';

export type Scope = 'admin' | 'room_owner' | 'dev' | 'bot_owner' | 'bot' | 'reviewer';
export interface Claim { key: string; value: any; }
export interface AuthDetails {
  RawToken: string;
  TokenData: any;
  IsAuthenticated: boolean;
  Scopes: Scope[];
  Claims: Claim[];
  GetClaim: (claim: string) => any;
  HasScope: (scope: Scope) => boolean;
}

@Injectable()
export class AuthService {
  private static nonScopeClaims = ['unique_name', 'accountId', 'nbf', 'exp', 'iat'];

  private subject: BehaviorSubject<AuthDetails> = new BehaviorSubject<AuthDetails>(this.getAuthDetails());
  constructor(private config: Configuration, private authenticationService: AuthenticationService) {
  }

  public GetAuthDetails(): Observable<AuthDetails> {
    return this.subject;
  }

  private getAuthDetails(): AuthDetails {
    const getAccessToken = () => {
      const accessTokenGetter = this.config.accessToken;
      if (!accessTokenGetter) {
        return undefined;
      }
      if (typeof accessTokenGetter === 'function') {
        return accessTokenGetter();
      } else {
        return accessTokenGetter;
      }
    };
    const accessToken = getAccessToken();
    let payload = null;
    if (accessToken) {
      const tokenPayload = accessToken.split('.')[1];
      payload = JSON.parse(atob(tokenPayload));
      payload = this.HandleExpiryDates(payload);
    }

    const scopes: Scope[] = [];
    const claims: Claim[] = [];
    if (payload) {
      const keys = Object.keys(payload);
      keys.forEach(key => {
        if (AuthService.nonScopeClaims.indexOf(key) < 0) {
          scopes.push(key as Scope);
        }
        claims.push({ key: key, value: payload[key] });
      });
    }
    const getClaim = (claim: string) => {
      const matchingClaim = claims.find(a => a.key === claim);
      if (matchingClaim) {
        return matchingClaim.value;
      }
    };

    return {
      RawToken: accessToken,
      TokenData: payload,
      IsAuthenticated: !!payload,
      Scopes: scopes,
      Claims: claims,
      GetClaim: getClaim,
      HasScope: (scope: Scope) => !!payload && scopes.indexOf(scope) >= 0
    };
  }

  private HandleExpiryDates(payload: any) {
    const now = new Date();
    if (!payload || !payload.exp) {
      return undefined;
    } else {
      const expDate = new Date(payload.exp * 1000);
      // Typescript doesn't recognize that dates can be subtracted
      let timeToRefresh = expDate as any - (now as any);
      timeToRefresh -= (1000 * 60 * 5); // Refresh 5 minutes before expiry
      if (timeToRefresh < 0) {
        return undefined;
      }
      setTimeout(() => {
        this.authenticationService.authenticationRefreshTokenPost().subscribe(token => {
          this.Login(token);
        });
      }, timeToRefresh);
      return payload;
    }
  }

  public Login(token: string) {
    localStorage.setItem(AccessTokenStorageKey, token);
    this.subject.next(this.getAuthDetails());
  }
  public Logout() {
    localStorage.removeItem(AccessTokenStorageKey);
    this.subject.next(this.getAuthDetails());
  }
}
