import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { GameComment } from '../../core/models/game-comment';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';

@Component({
  selector: 'app-comment-tree',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatIconModule, MatMenuModule],
  templateUrl: './comment-tree.component.html',
  styleUrl: './comment-tree.component.css',
})
export class CommentTreeComponent {
  @Input() comments: GameComment[] = [];

  quoteComment(comment: GameComment) {
    console.log(this.comments);
    console.log('Quote:', comment);
  }

  replyToComment(comment: GameComment) {
    console.log('Reply to:', comment);
  }

  banUser(comment: GameComment) {
    console.log('Ban user:', comment);
  }
}
