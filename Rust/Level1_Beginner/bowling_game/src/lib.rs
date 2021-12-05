#[allow(dead_code)]
struct Game {
    score: u32,
}

#[allow(dead_code)]
impl Game {
    fn roll(&mut self, pins: u32) {
        self.score += pins;
    }

    fn score(&self) -> u32 {
        return self.score;
    }
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn one_roll() {
        let mut game = Game { score: 0 };
        game.roll(3);

        assert_eq!(3, game.score());
    }

    #[test]
    fn two_roll() {
        let mut game = Game { score: 0 };
        game.roll(1);
        game.roll(2);

        assert_eq!(3, game.score());
    }

    #[test]
    fn gutter_game() {
        let mut game = Game { score: 0 };
        for _n in 1..20 {
            game.roll(0);
        }

        assert_eq!(0, game.score());
    }
}
