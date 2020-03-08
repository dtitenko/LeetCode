# Given a smaller string s and a bigger string b, design an algorithm to find all permutations
# of the shorter string withing the longer one. Print the locations of each permutation.

def permutations(s = "", b = ""):
    chars = {char : s.count(char) for char in s}
    window = dict()
    right, left, formed, sub = 0, 0, 0, 0
    while right < len(b):
        c = b[right]
        if c not in chars:
            window.clear()
            right += 1
            left = right
            formed = 0
            sub = 0
            continue
        else:
            matched = window.get(c, 0) == chars[c]
            window[c] = window.get(c, 0) + 1
            sub += 1
            if window[c] == chars[c]:
                formed += 1
            elif matched:
                formed -= 1
            if formed == len(chars):
                print(b[left:right + 1])
            if sub > len(s):
                c = b[left]
                matched = window[c] == chars[c]
                window[c] -= 1
                sub -= 1
                if window[c] == chars[c]:
                    formed += 1
                elif matched:
                    formed -= 1
                left += 1
                if formed == len(chars):
                    print(b[left:right + 1])
        right += 1

permutations("abbc", "cbabadcbbabbcbabaabccbabc")
