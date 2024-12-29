import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ConfigService } from './config.service';
import { Observable } from 'rxjs';
import { GameComment } from '../models/game-comment';

@Injectable({
  providedIn: 'root',
})
export class CommentService {
  constructor(private http: HttpClient, private config: ConfigService) {}

  getByGameKey(gameKey: string): Observable<any> {
    return this.http.get(this.config.getCommentsByGameKeyApiUrl(gameKey));
  }

  addComment(comment: GameComment, gameKey: string): Observable<any> {
    let request = {
      comment: comment,
      parentId: comment.parentId,
    };

    return this.http.post(this.config.addCommentApiUrl(gameKey), request);
  }
}
