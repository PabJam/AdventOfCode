pub fn part_1(input: String) -> u64
{
    let lines = input.lines();
    let mut sum: u64 = 0;
    for line in lines
    {
        let mut matchup = line.split_ascii_whitespace();
        let opponent = matchup.next().unwrap().as_bytes()[0] - 'A' as u8;
        let me = matchup.next().unwrap().as_bytes()[0] - 'X' as u8;

        let mut inter: u64 = (me + 1) as u64;
        if me == opponent {inter += 3;}
        else if me == opponent + 1 {inter += 6;}
        else if me == 0 && opponent == 2 {inter += 6;}

        sum += inter;
    }
    sum
}

pub fn part_2(input: String) -> u64
{
    let lines = input.lines();
    let mut sum: u64 = 0;
    for line in lines
    {
        let mut matchup = line.split_ascii_whitespace();
        let opponent = matchup.next().unwrap().as_bytes()[0] - 'A' as u8;
        let outcome = matchup.next().unwrap().chars().next();

        match outcome
        {
            Some('X') => sum += 1 + ((opponent + 2) % 3)  as u64,
            Some('Y') => sum += 4 + opponent as u64,
            Some('Z') => sum += 7 + ((opponent + 1) % 3)  as u64,
            None | Some(_) => ()
        }
    }
    sum
}