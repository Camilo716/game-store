import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { firstValueFrom, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ConfigService {
  private config: any;

  constructor(private http: HttpClient) {}

  async loadConfig(): Promise<void> {
    return firstValueFrom(
      this.http.get('/assets/configuration/configuration.json')
    )
      .then((data: any) => {
        this.config = data;
      })
      .catch((error: any) => {
        console.error('Failed to load config:', error);
      });
  }

  deleteGameApiUrl(id: string) {
    return `${this.config.baseApiUrl}${this.config.gamesApiUrl}/${id}`;
  }

  updateGameApiUrl() {
    return `${this.config.baseApiUrl}${this.config.gamesApiUrl}`;
  }

  addGameApiUrl(): string {
    return `${this.config.baseApiUrl}${this.config.gamesApiUrl}`;
  }

  getGamesApiUrl(): string {
    return `${this.config.baseApiUrl}${this.config.gamesApiUrl}`;
  }

  getGamesByPublisherIdApiUrl(publisherId: string): string {
    return `${this.config.baseApiUrl}${this.config.publishersApiUrl}/${publisherId}/${this.config.gamesApiUrl}`;
  }

  getGamesByGenreIdApiUrl(genreId: string) {
    return `${this.config.baseApiUrl}${this.config.genresApiUrl}/${genreId}/${this.config.gamesApiUrl}`;
  }

  getGamesByPlatformIdApiUrl(platformId: string) {
    return `${this.config.baseApiUrl}${this.config.platformsApiUrl}/${platformId}/${this.config.gamesApiUrl}`;
  }

  getGenresApiUrl(): string {
    return `${this.config.baseApiUrl}${this.config.genresApiUrl}`;
  }

  deleteGenreApiUrl(id: string) {
    return `${this.config.baseApiUrl}${this.config.genresApiUrl}/${id}`;
  }

  updateGenreApiUrl() {
    return `${this.config.baseApiUrl}${this.config.genresApiUrl}`;
  }

  addGenreApiUrl(): string {
    return `${this.config.baseApiUrl}${this.config.genresApiUrl}`;
  }

  getGenresByGameKeyApiUrl(key: string): string {
    return `${this.config.baseApiUrl}${this.config.gamesApiUrl}/${key}/${this.config.genresApiUrl}`;
  }

  getPlatformsApiUrl(): string {
    return `${this.config.baseApiUrl}${this.config.platformsApiUrl}`;
  }

  getPlatformsByGameKeyApiUrl(key: string): string {
    return `${this.config.baseApiUrl}${this.config.gamesApiUrl}/${key}/${this.config.platformsApiUrl}`;
  }

  getCommentsByGameKeyApiUrl(key: string): string {
    return `${this.config.baseApiUrl}${this.config.gamesApiUrl}/${key}/${this.config.commentsApiUrl}`;
  }

  addCommentApiUrl(gameKey: string): string {
    return `${this.config.baseApiUrl}${this.config.gamesApiUrl}/${gameKey}/${this.config.commentsApiUrl}`;
  }

  deleteCommentApiUrl(id: string) {
    return `${this.config.baseApiUrl}${this.config.gamesApiUrl}/${this.config.commentsApiUrl}/${id}`;
  }

  deletePlatformApiUrl(id: string) {
    return `${this.config.baseApiUrl}${this.config.platformsApiUrl}/${id}`;
  }

  updatePlatformApiUrl() {
    return `${this.config.baseApiUrl}${this.config.platformsApiUrl}`;
  }

  addPlatformApiUrl(): string {
    return `${this.config.baseApiUrl}${this.config.platformsApiUrl}`;
  }

  getPublishersApiUrl(): string {
    return `${this.config.baseApiUrl}${this.config.publishersApiUrl}`;
  }

  getPublishersByGameKeyApiUrl(key: string): string {
    return `${this.config.baseApiUrl}${this.config.gamesApiUrl}/${key}/${this.config.publisherApiUrl}`;
  }

  deletePublisherApiUrl(id: string) {
    return `${this.config.baseApiUrl}${this.config.publishersApiUrl}/${id}`;
  }

  updatePublisherApiUrl() {
    return `${this.config.baseApiUrl}${this.config.publishersApiUrl}`;
  }

  addPublisherApiUrl(): string {
    return `${this.config.baseApiUrl}${this.config.publishersApiUrl}`;
  }

  getCartApiUrl(): string {
    return `${this.config.baseApiPaymentUrl}${this.config.ordersApiUrl}/cart`;
  }

  getOrdersApiUrl(): string {
    return `${this.config.baseApiPaymentUrl}${this.config.ordersApiUrl}`;
  }

  addToCartApiUrl(gameKey: string): string {
    return `${this.config.baseApiPaymentUrl}${this.config.gamesApiUrl}/${gameKey}/buy`;
  }

  getOrderDetailsApiUrl(orderId: string): string {
    return `${this.config.baseApiPaymentUrl}${this.config.ordersApiUrl}/${orderId}/details`;
  }

  getPaymentMethodApiUrl(): string {
    return `${this.config.baseApiPaymentUrl}${this.config.ordersApiUrl}/payment-methods`;
  }

  payOrderApiUrl(): string {
    return `${this.config.baseApiPaymentUrl}${this.config.ordersApiUrl}/payment`;
  }

  loginApiUrl(): string {
    return `${this.config.baseApiAuth}${this.config.usersApiUrl}/login`;
  }

  getUsersApiUrl(): string {
    return `${this.config.baseApiAuth}${this.config.usersApiUrl}`;
  }

  getUserRolesApiUrl(id: string): string {
    return `${this.config.baseApiAuth}${this.config.usersApiUrl}/${id}/roles`;
  }

  updateUserApiUrl(): string {
    return `${this.config.baseApiAuth}${this.config.usersApiUrl}`;
  }

  addUserApiUrl(): string {
    return `${this.config.baseApiAuth}${this.config.usersApiUrl}`;
  }

  banUserApiUrl(userName: string): string {
    return `${this.config.baseApiAuth}${this.config.usersApiUrl}/${userName}/ban`;
  }

  getUserBanDurationsApiUrl(): string {
    return `${this.config.baseApiAuth}${this.config.usersApiUrl}/ban/durations`;
  }

  deleteUserApiUrl(id: string): string {
    return `${this.config.baseApiAuth}${this.config.usersApiUrl}/${id}`;
  }

  getRolesApiUrl(): string {
    return `${this.config.baseApiAuth}${this.config.rolesApiUrl}`;
  }

  getRolePermissionsApiUrl(roleId: string): string {
    return `${this.config.baseApiAuth}${this.config.rolesApiUrl}/${roleId}/permissions`;
  }

  getPermissionsApiUrl(): string {
    return `${this.config.baseApiAuth}${this.config.rolesApiUrl}/permissions`;
  }

  updateRoleApiUrl(): string {
    return `${this.config.baseApiAuth}${this.config.rolesApiUrl}`;
  }

  addRoleApiUrl(): string {
    return `${this.config.baseApiAuth}${this.config.rolesApiUrl}`;
  }

  deleteRoleApiUrl(id: string): string {
    return `${this.config.baseApiAuth}${this.config.rolesApiUrl}/${id}`;
  }
}
