public enum GameFlow
{
    SELECT_SEAT,
    BETTING,
    CARD_DISTRIBUTION,
    CHOICE,
    CARD_COMPARE,
    TOTAL,
}

public enum GameResult
{
    NONE,
    BLACKJACK,
    WIN,
    DRAW,
    LOSE,
    BUST
}

public enum BetAction
{
    NONE,
    HIT,
    STAND,
    SPLIT,
    DOUBLE_DOWN,
}

public enum CardShape
{
    NONE,
    CLUB,
    HEART,
    SPADE,
    DIAMOND,
}

public enum CamerasNumber
{
    CAM_NORMAL,
    CAM_TABLE,
}