# Given a smaller string s and a bigger string b, design an algorithm to find all permutations
# of the shorter string withing the longer one. Print the locations of each permutation.

# using Rabin-Karp Substring Search

def permutations(s: str, b: str) -> None:
    sLen = len(s)
    bLen = len(b)
    if sLen > bLen:
        raise Exception('s > b')
    sHash = 0
    bHash = 0
    # prepare first len(s) symbols hash
    for i in range(sLen):
        sHash += charHash(s[i])
        bHash += charHash(b[i])
    if sHash == bHash and isPermutation(s, b[0:sLen]):
        print(b[0:sLen])
    for i in range(sLen, bLen):
        # slide the window removing hash of the first symbol and adding next symbol
        bHash = bHash - charHash(b[i - sLen]) + charHash(b[i])
        start = i - sLen + 1
        if sHash == bHash and isPermutation(s, b[start:start + sLen]):
            print(b[start:start + sLen])


def charHash(char):
    return ord(char) % 26

def isPermutation(s1, s2):
    if len(s1) != len(s2) or len(s1) == 0:
        return False
    if sorted(s1) == sorted(s2):
        return True


permutations("abbc", "cbabadcbbabbcbabaabccbabc")
